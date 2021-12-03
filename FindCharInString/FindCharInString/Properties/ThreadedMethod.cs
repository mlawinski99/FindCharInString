using System;

public class ThreadedMethod<T>
{

    private T result;
    public T Result
    {
        get { return result; }
        private set { result = value; }
    }

    public ThreadedMethod()
    {
    }

    public void ExecuteMethod(Func<T> func)
    {
        Result = func.Invoke();
    }
    public void ExecuteMethod(Delegate d)
    {
        Result = (T)d.DynamicInvoke();
    }
}