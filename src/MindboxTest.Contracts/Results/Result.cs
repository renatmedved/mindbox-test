using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Linq;

namespace MindboxTest.Contracts.Results
{
    public sealed class Result<TData>
    {
        private readonly TData _data;
        private readonly ReadOnlyCollection<string> _errs;

        private Result(IEnumerable<string> errs, TData data)
        {
            _errs = errs?.ToList()?.AsReadOnly();
            _data = data;
        }

        public static Result<TData> MakeSucces(TData data)
        {
            return new Result<TData>(null, data);
        }

        public static Result<TData> MakeFailMessage(string err)
        {
            return MakeFail(new[] { err });
        }

        public static Result<TData> MakeFail(IEnumerable<string> errs)
        {
            if (errs == null)
            {
                throw new ArgumentNullException($"{nameof(errs)} should not be null");
            }

            return new Result<TData>(errs, default);
        }

        public ReadOnlyCollection<string> Errors
        {
            get
            {
                if (_errs == null)
                {
                    throw new InvalidOperationException($"property {nameof(Success)} should be false to read property {nameof(Errors)}");
                }

                return _errs;
            }
        }
        public TData Data
        {
            get
            {
                if (_errs != null)
                {
                    throw new InvalidOperationException($"property {nameof(Success)} should be true to read property {nameof(Data)}");
                }

                return _data;
            }
        }
        public bool Success => _errs == null;
        public bool Fail => !Success;
    }
}
