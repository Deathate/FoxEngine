
using UnityEngine;
using System;

namespace FoxEngine {
    public class FoxCounter {
        public class FoxCounterHidden : MonoBehaviour {
            Action mission;
            public void SetMission(Action act) => mission = act;
            Action lastword;
            public void SetLastword(Action act) => lastword = act;
            float interval;
            public void SetInterval(float f) => interval = f;
            bool repeat;
            public void SetRepeat(bool b) => repeat = b;
            float delay;
            public void SetDelay(float f) => delay = f;
            int times;
            public void SetTimes(int f) => times = f;
            float lifetime;
            public void SetLifetime(float f) => lifetime = f;
            Func<bool> diecondition;
            public void SetDieCondition(Func<bool> act) => diecondition = act;
            float counter;

            public int id;

            public FoxCounterHidden Initial() {
                this.mission = null;
                this.lastword = null;
                this.interval = 0;
                this.counter = Time.time;
                this.repeat = false;
                this.delay = Time.time;
                this.times = -1;
                this.lifetime = Mathf.Infinity;
                this.diecondition = () => false;
                this.id = -1;
                return this;
            }

            private void Update() {
                if (Time.time < delay) return;
                if (lifetime < Time.time - delay) Delete();
                if (Time.time >= counter + interval) {
                    counter += interval;
                    mission?.Invoke();
                    if (!repeat)
                        Delete();
                    if (times != -1) {
                        --times;
                        if (times == 0) {
                            Delete();
                        }
                    }
                }
                if (diecondition()) Delete();
            }

            public void Delete() {
                lastword?.Invoke();
                foxCounterPool.Restitution(this);
            }
        }

        // variable
        static FoxComponentPool<FoxCounterHidden> foxCounterPool = new FoxComponentPool<FoxCounterHidden>();
        public FoxCounterHidden foxCounterHidden;

        // constructor
        public FoxCounter() { foxCounterHidden = foxCounterPool.Rent(); foxCounterHidden.Initial(); }
        public FoxCounter(float interval, Action mission) : this() {
            foxCounterHidden.SetInterval(interval);
            foxCounterHidden.SetMission(mission);
        }
        public FoxCounter(Action mission) : this() { foxCounterHidden.SetMission(mission); }

        // method
        public FoxCounter SetMission(Action action) { foxCounterHidden.SetMission(action); return this; }
        public FoxCounter SetTimes(int times) { foxCounterHidden.SetTimes(times); foxCounterHidden.SetRepeat(true); return this; }
        public FoxCounter SetInterval(float interval) { foxCounterHidden.SetInterval(interval); return this; }
        public FoxCounter SetDelay(float t) { foxCounterHidden.SetDelay(t + Time.time); return this; }
        public FoxCounter SetLifeTime(float seconds) { foxCounterHidden.SetLifetime(seconds); foxCounterHidden.SetRepeat(true); return this; }
        public FoxCounter SetRepeat(bool b) { foxCounterHidden.SetRepeat(b); return this; }
        public FoxCounter SetLastword(Action action) { foxCounterHidden.SetLastword(action); return this; }
        public FoxCounter SetDieCondition(Func<bool> action) { foxCounterHidden.SetDieCondition(action); foxCounterHidden.SetRepeat(true); return this; }
        public FoxCounter SetId(int id) { foxCounterHidden.id = id; return this; }
        public FoxCounterHidden Get() => foxCounterHidden;
        public void Delete() {
            foxCounterHidden.Delete();
            foxCounterHidden = null;
        }
    }
}