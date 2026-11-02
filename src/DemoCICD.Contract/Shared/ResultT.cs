namespace DemoCICD.Contract.Shared
{
    public class ResultT<TValue> : Result
    {
        private readonly TValue? _value;

        protected internal ResultT(TValue? value, bool isSuccess, Error error)
            : base(isSuccess, error)
        {
            this._value = value;
        }

        public TValue Value
        {
            get
            {
                if (this.IsFailure)
                {
                    throw new InvalidOperationException("The value of a failed result cannot be accessed.");
                }

                return this._value!;
            }
        }

        public static implicit operator ResultT<TValue>(TValue? value) => Create(value);
    }
}
