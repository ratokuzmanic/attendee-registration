using System;

namespace DumpDays.AttendeeRegistration.Common
{
    public interface IMaybe<out T>
    {
        bool HasValue { get; }
        TU   Case<TU> (Func<T, TU> some, Func<TU> none);
        void Case     (Action<T> some, Action none);
    }

    public static class Some<T>
    {
        public static IMaybe<T> Exists(T value)
            => new Maybe<T>(value);
    }

    public static class None<T>
    {
        public static IMaybe<T> Exists
            => new Maybe<T>();
    }

    public class Maybe<T> : IMaybe<T>
    {
        public  bool HasValue { get; }
        private T    Value    { get; }

        public Maybe(T value)
        {
            Value = value;
            HasValue = true;
        }
        
        public Maybe()
        {
            HasValue = false;
        }

        public TU Case<TU>(Func<T, TU> some, Func<TU> none) 
            => HasValue ? some(Value) : none();

        public void Case(Action<T> some, Action none)
        {
            if (HasValue) some(Value);
            else none();
        }
    }
}
