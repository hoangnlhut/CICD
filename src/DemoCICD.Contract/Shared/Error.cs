namespace DemoCICD.Contract.Shared
{
    public class Error : IEquatable<Error>
    {
        public string Code { get; }
        public string Message { get; }
        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public bool Equals(Error? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Code == other.Code && Message == other.Message;
        }
        public override bool Equals(object? obj)
        {
            return Equals(obj as Error);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Code, Message);
        }
    }
}
