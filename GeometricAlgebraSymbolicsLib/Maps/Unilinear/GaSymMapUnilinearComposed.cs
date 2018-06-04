using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraSymbolicsLib.Exceptions;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;

namespace GeometricAlgebraSymbolicsLib.Maps.Unilinear
{
    public sealed class GaSymMapUnilinearComposed : GaSymMapUnilinear
    {
        public static GaSymMapUnilinearComposed CreateIdentity(int vSpaceDim)
        {
            return new GaSymMapUnilinearComposed(vSpaceDim);
        }

        public static GaSymMapUnilinearComposed Create(IGaSymMapUnilinear firstMappings)
        {
            return new GaSymMapUnilinearComposed(firstMappings);
        }

        public static GaSymMapUnilinearComposed Create(params IGaSymMapUnilinear[] mappingsList)
        {
            var resultMap = new GaSymMapUnilinearComposed(mappingsList[0]);

            for (var i = 1; i < mappingsList.Length; i++)
                resultMap.AddMapping(mappingsList[i]);

            return resultMap;
        }

        public static GaSymMapUnilinearComposed Create(IEnumerable<IGaSymMapUnilinear> mappingsList)
        {
            var mappingsArray = mappingsList.ToArray();
            var resultMap = new GaSymMapUnilinearComposed(mappingsArray[0]);

            for (var i = 1; i < mappingsArray.Length; i++)
                resultMap.AddMapping(mappingsArray[i]);

            return resultMap;
        }


        private readonly List<IGaSymMapUnilinear> _mappingsList
            = new List<IGaSymMapUnilinear>();


        public override int TargetVSpaceDimension 
            => _mappingsList[_mappingsList.Count - 1].TargetVSpaceDimension;

        public override int DomainVSpaceDimension
            => _mappingsList[0].DomainVSpaceDimension;

        public IEnumerable<IGaSymMapUnilinear> Mappings 
            => _mappingsList;

        public override IGaSymMultivector this[int id1]
        {
            get
            {
                var resultMv = _mappingsList[0][id1].ToMultivector();

                for (var i = 1; i < _mappingsList.Count; i++)
                    resultMv = _mappingsList[i][resultMv];

                return resultMv;
            }
        }
        

        public override IGaSymMultivectorTemp MapToTemp(int id1)
        {
            var resultMv = _mappingsList[0][id1].ToMultivector();

            for (var i = 1; i < _mappingsList.Count - 1; i++)
                resultMv = _mappingsList[i][resultMv];

            return _mappingsList[_mappingsList.Count - 1].MapToTemp(resultMv);
        }


        private GaSymMapUnilinearComposed(int vSpaceDim)
        {
            _mappingsList.Add(GaSymMapUnilinearIdentity.Create(vSpaceDim));
        }

        private GaSymMapUnilinearComposed(IGaSymMapUnilinear firstMapping)
        {
            _mappingsList.Add(firstMapping);
        }


        public GaSymMapUnilinearComposed ResetToIdentity(int vSpaceDim)
        {
            _mappingsList.Clear();
            _mappingsList.Add(GaSymMapUnilinearIdentity.Create(vSpaceDim));

            return this;
        }

        public GaSymMapUnilinearComposed ResetToMapping(IGaSymMapUnilinear firstMapping)
        {
            _mappingsList.Clear();
            _mappingsList.Add(firstMapping);

            return this;
        }

        public GaSymMapUnilinearComposed AddMappings(params IGaSymMapUnilinear[] mappingsArray)
        {
            foreach (var map in mappingsArray)
                AddMapping(map);

            return this;
        }

        public GaSymMapUnilinearComposed AddMappings(IEnumerable<IGaSymMapUnilinear> mappingsList)
        {
            foreach (var map in mappingsList)
                AddMapping(map);

            return this;
        }

        public GaSymMapUnilinearComposed AddMapping(IGaSymMapUnilinear map)
        {
            if (_mappingsList.Count > 0 && TargetVSpaceDimension != map.DomainVSpaceDimension)
                throw new InvalidOperationException("Mapping space dimensions mismatch");

            _mappingsList.Add(map);

            return this;
        }


        public override IGaSymMultivectorTemp MapToTemp(GaSymMultivector mv1)
        {
            if (mv1.GaSpaceDimension != DomainGaSpaceDimension)
                throw new GaSymbolicsException("Multivector size mismatch");

            var resultMv = _mappingsList[0][mv1];

            for (var i = 1; i < _mappingsList.Count - 1; i++)
                resultMv = _mappingsList[i][resultMv];

            return _mappingsList[_mappingsList.Count - 1].MapToTemp(resultMv);
        }

        public override IEnumerable<Tuple<int, IGaSymMultivector>> BasisBladeMaps()
        {
            return 
                Enumerable
                .Range(0, DomainGaSpaceDimension)
                .Select(id => new Tuple<int, IGaSymMultivector>(id, this[id]))
                .Where(t => !t.Item2.IsNullOrZero());
        }
    }
}
