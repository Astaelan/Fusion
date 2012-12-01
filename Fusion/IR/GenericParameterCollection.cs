using System;
using System.Collections.Generic;

namespace Fusion.IR
{
    /// <summary>
    /// Represents a collection of type parameters to a generic type.
    /// </summary>
    public sealed class GenericParameterCollection : IEnumerable<IRType>
    {
        private const int InnerListInitialCapacity = 32;

        private readonly List<IRType> mParams = new List<IRType>(InnerListInitialCapacity);

        /// <summary>
        /// The number of parameters in this collection.
        /// </summary>
        public int Count
        {
            get { return mParams.Count; }
        }

        private bool? mResolvedCache;
        /// <summary>
        /// True if all the generic parameters
        /// in this collection are resolved.
        /// </summary>
        public bool Resolved
        {
            get
            {
                if (mResolvedCache != null)
                    return mResolvedCache.Value;

                mResolvedCache = mParams.TrueForAll(t => t.Resolved);
                return mResolvedCache.Value;
            }
        }


        public GenericParameterCollection() { }
        public GenericParameterCollection(params IRType[] existingParams) : this((IEnumerable<IRType>)existingParams) { }

        public GenericParameterCollection(IEnumerable<IRType> existingParams)
        {
            foreach (IRType t in existingParams)
            {
                mParams.Add(t);
            }
        }

        /// <summary>
        /// Create a shallow copy of this collection.
        /// </summary>
        /// <returns>The new collection.</returns>
        public GenericParameterCollection Clone()
        {
            return new GenericParameterCollection(this);
        }

        /// <summary>
        /// Add a Type to this collection of generic parameters.
        /// </summary>
        /// <param name="param">The type to add.</param>
        public void Add(IRType param)
        {
            mParams.Add(param);
            if (mResolvedCache != null)
            {
                if (mResolvedCache.Value && !param.Resolved) 
                    mResolvedCache = false;
            }
        }

        /// <summary>
        /// Add a set of Types to this collection of generic parameters.
        /// </summary>
        /// <param name="paramsToAdd">The types to add.</param>
        public void AddRange(IEnumerable<IRType> paramsToAdd)
        {
            mParams.AddRange(paramsToAdd);
            if (mResolvedCache != null)
            {
                if (mResolvedCache.Value)
                {
                    foreach (IRType t in paramsToAdd)
                    {
                        if (!t.Resolved)
                        {
                            mResolvedCache = false;
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Add a set of Types to this collection of generic parameters.
        /// </summary>
        /// <param name="paramsToAdd">The types to add.</param>
        public void AddRange(params IRType[] paramsToAdd)
        {
            this.AddRange((IEnumerable<IRType>)paramsToAdd);
        }

        public IRType this[int idx]
        {
            get
            {
                return mParams[idx];
            }
            set
            {
                if (mResolvedCache != null)
                {
                    if (mResolvedCache.Value && !value.Resolved)
                        mResolvedCache = false;
                }
                mParams[idx] = value;
            }
        }

        public IEnumerator<IRType> GetEnumerator()
        {
            foreach (IRType t in mParams)
                yield return t;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            foreach (IRType t in mParams)
                yield return t;
        }
    }
}
