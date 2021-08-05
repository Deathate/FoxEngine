
using UnityEngine;
using System.Collections.Generic;

namespace FoxEngine.Input {

    public class FoxInputManager {

        static Dictionary<string, bool> downstatedict = new Dictionary<string, bool>();
        static Dictionary<string, bool> upstatedict = new Dictionary<string, bool>();
        static Dictionary<string, bool> duringstatedict = new Dictionary<string, bool>();
        static Dictionary<string, float> downtimedict = new Dictionary<string, float>();
        static Dictionary<string, float> uptimedict = new Dictionary<string, float>();

        static void UpdateState(Dictionary<string, bool> statedict, string key, bool b) => statedict[key] = b;
        static void UpdateTimer(Dictionary<string, float> timedict, string key, float time) => timedict[key] = time;

        public static void RegisterKey(string s) {
            downstatedict.Add(s, false);
            upstatedict.Add(s, false);
            duringstatedict.Add(s, false);
            downtimedict.Add(s, 0);
            uptimedict.Add(s, 0);
        }

        public static void Press(string key) {
            UpdateState(downstatedict, key, true);
            UpdateState(duringstatedict, key, true);
            Unstated(downstatedict, key);
            UpdateTimer(downtimedict, key, Time.time);
        }
        public static void Release(string key) {
            UpdateState(upstatedict, key, true);
            UpdateState(duringstatedict, key, false);
            Unstated(upstatedict, key);
            UpdateTimer(uptimedict, key, Time.time);
        }
        static void Unstated(Dictionary<string, bool> statedict, string key) {
            int framecount = Time.frameCount;
            var f = new FoxCounter()
            .SetDieCondition(() => Time.frameCount > framecount)
            .SetLastword(() => {
                UpdateState(statedict, key, false);
            })
            .SetId(105);
        }

        public static bool GetKeyDown(string key) {
            if (downstatedict[key] && Time.time >= downtimedict[key]) {
                UpdateTimer(downtimedict, key, Time.time + 0.05f);
                return true;
            }
            return false;
        }
        public static bool GetKeyUp(string key) {
            if (upstatedict[key] && Time.time >= uptimedict[key]) {
                UpdateTimer(uptimedict, key, Time.time + 0.05f);
                return true;
            }
            return false;
        }
        public static bool GetKey(string key) {
            return duringstatedict[key];
        }
    }
}