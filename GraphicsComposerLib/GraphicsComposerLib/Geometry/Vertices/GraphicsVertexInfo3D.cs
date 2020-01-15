namespace GraphicsComposerLib.Geometry.Vertices
{
    public struct GraphicsVertexDataInfo3D
    {
        public static GraphicsVertexDataInfo3D CreateVertexDataInfo()
        {
            return new GraphicsVertexDataInfo3D(
                false, 
                false, 
                false
            );
        }

        public static GraphicsVertexDataInfo3D CreateColoredVertexDataInfo()
        {
            return new GraphicsVertexDataInfo3D(
                true, 
                false, 
                false
            );
        }

        public static GraphicsVertexDataInfo3D CreateTexturedVertexDataInfo()
        {
            return new GraphicsVertexDataInfo3D(
                false, 
                true, 
                false
            );
        }

        public static GraphicsVertexDataInfo3D CreateNormalVertexDataInfo()
        {
            return new GraphicsVertexDataInfo3D(
                false, 
                false, 
                true
            );
        }

        public static GraphicsVertexDataInfo3D CreateNormalColoredVertexDataInfo()
        {
            return new GraphicsVertexDataInfo3D(
                true, 
                false, 
                true
            );
        }

        public static GraphicsVertexDataInfo3D CreateNormalTexturedVertexDataInfo()
        {
            return new GraphicsVertexDataInfo3D(
                false, 
                true, 
                true
            );
        }


        public bool HasColor { get; }

        public bool HasTextureUv { get; }

        public bool HasNormal { get; }


        private GraphicsVertexDataInfo3D(bool hasColor, bool hasTextureUv, bool hasNormal)
        {
            HasColor = hasColor;
            HasTextureUv = hasTextureUv;
            HasNormal = hasNormal;
        }
    }
}
