using System.Collections;
using System.Collections.Generic;

namespace TheGreatFollowUpper.Util
{
    public class Linqqer<TCollection, UType> : IEnumerable<UType>, IEnumerator<UType>
        where TCollection : IEnumerable
    {
        private readonly TCollection _collection;
        private readonly IEnumerator _enumerator;

        public Linqqer(TCollection collection)
        {
            _collection = collection;
            _enumerator = _collection.GetEnumerator();
        }

        #region IEnumerable<U> Members

        public IEnumerator<UType> GetEnumerator()
        {
            return this;
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        #endregion

        #region IEnumerator<U> Members

        public UType Current
        {
            get { return (UType)_enumerator.Current; }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {

        }

        #endregion

        #region IEnumerator Members

        object IEnumerator.Current
        {
            get { return _enumerator.Current; }
        }

        public bool MoveNext()
        {
            bool res = _enumerator.MoveNext();

            while (res == true && !(_enumerator.Current is UType))
            {
                res = _enumerator.MoveNext();
            }

            return res;
        }

        public void Reset()
        {
            _enumerator.Reset();
        }

        #endregion
    }
}
