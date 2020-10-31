using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace StandaloneInputSystem
{
    public class MousePoint : MonoBehaviour
    {
        private EventTrigger trigger;
        public MouseBMoveType mouseBMoveType;
        private EventTrigger.Entry enter, exit;
        private bool actived;

        private void Start()
        {
            trigger = GetComponent<EventTrigger>();
            UnityAction<BaseEventData> e = new UnityAction<BaseEventData>(OnPointEnter);
            enter = new EventTrigger.Entry();
            enter.eventID = EventTriggerType.PointerEnter;
            enter.callback.AddListener(e);
            trigger.triggers.Add(enter);

            UnityAction<BaseEventData> ex = new UnityAction<BaseEventData>(OnPointExit);
            exit = new EventTrigger.Entry();
            exit.eventID = EventTriggerType.PointerExit;
            exit.callback.AddListener(ex);
            trigger.triggers.Add(exit);
        }

        private void OnPointExit(BaseEventData arg0)
        {
            actived = false;
        }

        private void OnPointEnter(BaseEventData arg0)
        {
            actived = true;
        }

        private void Update()
        {
            if (actived)
            {
                Camera.main.transform.position += GetDir() * Time.deltaTime * 0.5f;
            }
        }

        public Vector3 GetDir()
        {
            Vector2 vector = Vector2.zero;
            switch (mouseBMoveType)
            {
                case MouseBMoveType.None:
                    break;
                case MouseBMoveType.Up:
                    vector = Vector2.up;
                    break;
                case MouseBMoveType.Down:
                    vector = Vector2.down;
                    break;
                case MouseBMoveType.Left:
                    vector = -Vector2.left;
                    break;
                case MouseBMoveType.Right:
                    vector = -Vector2.right;
                    break;
                case MouseBMoveType.UpLeft:
                    vector = Vector2.up - Vector2.left;
                    break;
                case MouseBMoveType.UpRight:
                    vector = Vector2.up - Vector2.right;
                    break;
                case MouseBMoveType.DownLeft:
                    vector = Vector2.down - Vector2.left;
                    break;
                case MouseBMoveType.DownRight:
                    vector = Vector2.down - Vector2.left;
                    break;
                default:
                    break;
            }

            return vector;
        }

        private void OnApplicationFocus(bool focus)
        {
            if (focus)
            {
            }
            else
            {
                actived = false;
            }

        }
    }
}