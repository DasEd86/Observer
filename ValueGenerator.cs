using System;
using System.Collections.Generic;

class ValueGenerator : IObservable<int> {
   // Liste der Observer, die an Änderungen interessiert sind
   private List<IObserver<int>> observers;

    public ValueGenerator() {
        this.observers = new List<IObserver<int>>();
    }

    public IDisposable Subscribe(IObserver<int> observer) {
        // Observer in Liste hinzufügen wenn noch nicht vorhanden
        if (!observers.Contains(observer)) {
            observers.Add(observer);
        }
        return new Unsubscriber(observers, observer);
    }

    public void doSomething() {
        var rand = new Random();
        foreach (var observer in observers.ToArray()) {
            // Observer über neuen Wert informieren
            observer.OnNext(rand.Next(100));
        }
    }

    // Observer mitteilen, dass Observable keine Werte mehr sendet
    public void stop() {
        foreach (var observer in observers.ToArray()) {
            if (observers.Contains(observer)) {
                observer.OnCompleted();
            }
        }
        observers.Clear();
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
}
