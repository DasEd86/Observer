using System;

class Program
{
    static void Main(string[] args)
    {
      ValueGenerator provider = new ValueGenerator();
      ObserveValues reporter1 = new ObserveValues("Observable 1");
      reporter1.Subscribe(provider);
      ObserveValues reporter2 = new ObserveValues("Observable 2");
      reporter2.Subscribe(provider);

      provider.doSomething();
      provider.doSomething();
      provider.doSomething();
      provider.stop();
    }
}
