using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace UtilLib.Pools
{
    /// <summary>
    /// This class is an implementation of a generic universal object pool pattern. The only constraint on 
    /// the pooled objects is to implement the IPoolObject interface
    /// </summary>
    public static class MasterPool
    {
        /// <summary>
        /// A dictionary of pools for each key type in the application
        /// </summary>
        private static readonly Dictionary<Type, ConcurrentStack<IPoolObject>> PoolsDictionary = 
            new Dictionary<Type, ConcurrentStack<IPoolObject>>();


        ///// <summary>
        ///// Acquire an instance of an object of the given type and, if the given flag is true, 
        ///// call object's ResetOnAcquire() method
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="resetObject"></param>
        ///// <returns></returns>
        //public static T Acquire<T>(bool resetObject) where T : class, IPoolObject, new()
        //{
        //    T x = null;

        //    lock (PoolsDictionary)
        //    {
        //        ConcurrentStack<IPoolObject> stack;

        //        if (PoolsDictionary.TryGetValue(typeof(T), out stack))
        //        {
        //            IPoolObject obj;

        //            if (stack.TryPop(out obj))
        //                x = (T)obj;
        //        }
        //        else
        //        {
        //            PoolsDictionary.Add(typeof(T), new ConcurrentStack<IPoolObject>());
        //        }
        //    }

        //    if (x == null)
        //    {
        //        x = new T();
        //        x.InitializeOnCreate();
        //    }
        //    //else
        //    if (resetObject)
        //        x.ResetOnAcquire();

        //    return x;
        //}

        /// <summary>
        /// Acquire an instance of an object of the given type and call object's ResetOnAcquire() method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Acquire<T>() where T : class, IPoolObject, new()
        {
            T x = null;

            lock (PoolsDictionary)
            {
                ConcurrentStack<IPoolObject> stack;

                if (PoolsDictionary.TryGetValue(typeof(T), out stack))
                {
                    IPoolObject obj;

                    if (stack.TryPop(out obj))
                        x = (T) obj;
                }
                else
                {
                    PoolsDictionary.Add(typeof(T), new ConcurrentStack<IPoolObject>());
                }
            }

            if (x == null)
            {
                x = new T();

                if (x.EnableInitializeOnCreate)
                    x.InitializeOnCreate();
            }
            
            if (x.EnableResetOnAcquire)
                x.ResetOnAcquire();

            return x;
        }

        ///// <summary>
        ///// Release an object into the pool for later re-use and, if the given flag is true, 
        ///// call the object's ResetOnRelease() method
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="obj"></param>
        ///// <param name="resetObject"></param>
        //public static void Release<T>(T obj, bool resetObject) where T : class, IPoolObject
        //{
        //    if (ReferenceEquals(obj, null))
        //        throw new ArgumentNullException("obj", "Items added to a Pool cannot be null");

        //    ConcurrentStack<IPoolObject> stack;

        //    lock (PoolsDictionary)
        //        if (PoolsDictionary.TryGetValue(typeof(T), out stack))
        //        {
        //            if (resetObject)
        //                obj.ResetOnRelease();

        //            stack.Push(obj);

        //            return;
        //        }

        //    throw new Exception("ObjectPool.Release can not be called for object which is not created using ObjectPool.Acquire");
        //}

        /// <summary>
        /// Release an object into the pool for later re-use and call the object's ResetOnRelease() method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">A non-null reference to the released object</param>
        public static void Release<T>(T obj) where T : class, IPoolObject
        {
            if (ReferenceEquals(obj, null))
                throw new ArgumentNullException("obj", "Items added to a Pool cannot be null");

            ConcurrentStack<IPoolObject> stack;

            lock (PoolsDictionary)
                if (PoolsDictionary.TryGetValue(typeof (T), out stack))
                {
                    if (obj.EnableResetOnRelease)
                        obj.ResetOnRelease();

                    stack.Push(obj);

                    return;
                }

            throw new Exception("ObjectPool.Release can not be called for object which is not created using ObjectPool.Acquire");
        }

        /// <summary>
        /// Drop all objects of the given type from the master pool.
        /// This method will not call obj.ResetOnRelease for the dropped objects
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void DropAll<T>()
        {
            lock (PoolsDictionary)
            {
                ConcurrentStack<IPoolObject> stack;

                if (!PoolsDictionary.TryGetValue(typeof (T), out stack)) 
                    return;

                stack.Clear();

                PoolsDictionary.Remove(typeof(T));
            }
        }

        /// <summary>
        /// Drop all objects of all types in the master pool. 
        /// This method will not call obj.ResetOnRelease for the dropped objects
        /// </summary>
        public static void DropAll()
        {
            lock (PoolsDictionary)
            {
                foreach (var stack in PoolsDictionary.Values)
                    stack.Clear();

                PoolsDictionary.Clear();
            }
        }
    }
}
