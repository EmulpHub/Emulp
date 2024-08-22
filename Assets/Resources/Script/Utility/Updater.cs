using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace updaterNameSpace
{
    public class Updater : MonoBehaviour
    {
        private static Updater instance;

        public static Updater Instance
        {
            get
            {
                if (instance == null)
                    instance = GameObject.Find("Updater").GetComponent<Updater>();

                return instance;
            }
        }

        private static EventHandlerNoArg eventUpdate = new EventHandlerNoArg(false);

        public void Update()
        {
            eventUpdate.Call();
        }

        public void AddTo(EventHandlerNoArg.del toAdd)
        {
            V.eventLoadingNewScene.Add(() =>
            {
                eventUpdate.Add(toAdd);
            });

            eventUpdate.Add(toAdd);
        }
    }
}
