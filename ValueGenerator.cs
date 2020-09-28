using System;
using System.Collections.Generic;

class ValueGenerator : IObservable<int> {
   private List<IObserver<int>> observers;

    public ValueGenerator() {
        this.observers = new List<IObserver<int>>();
    }

    public IDisposable Subscribe(IObserver<int> observer) {
        if (!observers.Contains(observer)) {
            observers.Add(observer);
        }
        return new Unsubscriber(observers, observer);
    }

   private class Unsubscriber : IDisposable {
        private List<IObserver<int>>_observers;
        private IObserver<int> _observer;

        public Unsubscriber(List<IObserver<int>> observers, IObserver<int> observer) {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose() {
            if (_observer != null && _observers.Contains(_observer)) {
                _observers.Remove(_observer);
            }
        }
   }

    public void doSomething() {
        var rand = new Random();
        var val = rand.Next(100);
        foreach (var observer in observers.ToArray()) {
            if (val >= 50) {
                observer.OnNext(val);
            }
        }
    }

    public void stop() {
        foreach (var observer in observers.ToArray()) {
            if (observers.Contains(observer)) {
                observer.OnCompleted();
            }
        }
        observers.Clear();
    }
}
