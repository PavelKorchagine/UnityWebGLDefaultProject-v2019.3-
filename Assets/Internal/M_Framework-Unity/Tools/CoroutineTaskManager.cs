﻿/// TaskManager.cs
/// Copyright (c) 2011, Ken Rockot  <k-e-n-@-REMOVE-CAPS-AND-HYPHENS-oz.gs>.  All rights reserved.
/// Everyone is granted non-exclusive license to do anything at all with this code.
///
/// This is a new coroutine interface for Unity.
///
/// The motivation for this is twofold:
///
/// 1. The existing coroutine API provides no means of stopping specific
///    coroutines; StopCoroutine only takes a string argument, and it stops
///    all coroutines started with that same string; there is no way to stop
///    coroutines which were started directly from an enumerator.  This is
///    not robust enough and is also probably pretty inefficient.
///
/// 2. StartCoroutine and friends are MonoBehaviour methods.  This means
///    that in order to start a coroutine, a user typically must have some
///    component reference handy.  There are legitimate cases where such a
///    constraint is inconvenient.  This implementation hides that
///    constraint from the user.
///
/// Example usage:
///
/// ----------------------------------------------------------------------------
/// IEnumerator MyAwesomeTask()
/// {
///     while(true) {
///         Debug.Log("Logcat iz in ur consolez, spammin u wif messagez.");
///         yield return null;
////    }
/// }
///
/// IEnumerator TaskKiller(float delay, Task t)
/// {
///     yield return new WaitForSeconds(delay);
///     t.Stop();
/// }
///
/// void SomeCodeThatCouldBeAnywhereInTheUniverse()
/// {
///     Task spam = new Task(MyAwesomeTask());
///     new Task(TaskKiller(5, spam));
/// }
/// ----------------------------------------------------------------------------
///
/// When SomeCodeThatCouldBeAnywhereInTheUniverse is called, the debug console
/// will be spammed with annoying messages for 5 seconds.
///
/// Simple, really.  There is no need to initialize or even refer to TaskManager.
/// When the first Task is created in an application, a "TaskManager" GameObject
/// will automatically be added to the scene root with the TaskManager component
/// attached.  This component will be responsible for dispatching all coroutines
/// behind the scenes.
///
/// Task also provides an event that is triggered when the coroutine exits.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#region using UnityEditor;
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

namespace MFramework_Unity.Tools
{
    /// <summary>
    /// 协程管理器
    /// </summary>
    public class CoroutineTaskManager : MonoBehaviour
    {
        private static bool initialized;
        public class TaskState
        {
            public bool Running
            {
                get
                {
                    return running;
                }
            }

            public bool Paused
            {
                get
                {
                    return paused;
                }
            }
            public delegate void FinishedHandler(bool manual);
            public event FinishedHandler Finished;

            private IEnumerator coroutine;
            private bool running;
            private bool paused;
            private bool stopped;

            public TaskState(IEnumerator c)
            {
                coroutine = c;
            }

            public void Pause()
            {
                paused = true;
            }

            public void Unpause()
            {
                paused = false;
            }

            public Coroutine Start()
            {
                running = true;
                return singleton.StartCoroutine(CallWrapper());
            }

            public void Stop()
            {
                stopped = true;
                running = false;
            }

            private IEnumerator CallWrapper()
            {
                yield return null;
                IEnumerator e = coroutine;
                while (running)
                {
                    if (paused)
                        yield return null;
                    else
                    {
                        if (e != null && e.MoveNext())
                        {
                            yield return e.Current;
                        }
                        else
                        {
                            running = false;
                        }
                    }
                }

                FinishedHandler handler = Finished;
                if (handler != null)
                    handler(stopped);
            }
        }

        private static CoroutineTaskManager singleton;

        public static CoroutineTaskManager Instance
        {
            get
            {
                Initialize();
                return singleton;
            }
        }
        private void Awake()
        {
            if (gameObject.scene.name != "DontDestroyOnLoad")
            {
                DontDestroyOnLoad(this.gameObject);
            }
            initialized = true;
            singleton = this;
        }
        public void SayHello()
        {
            Debug.LogWarningFormat("[{0}] say: Hello，World !!", this);
        }
        private static void Initialize()
        {
            if (!initialized)
            {

                if (!Application.isPlaying)
                    return;
                initialized = true;
                var g = new GameObject("CoroutineTaskManager - Singleton - DontDestroyOnLoad");
                singleton = g.AddComponent<CoroutineTaskManager>();
            }
        }
        private Dictionary<IEnumerator, TaskState> taskStateDict = new Dictionary<IEnumerator, TaskState>();
        public TaskState CreateTask(IEnumerator coroutine)
        {
            if (singleton == null)
            {
                GameObject go = new GameObject("TaskManager - Singleton - DontDestroyOnLoad");
                singleton = go.AddComponent<CoroutineTaskManager>();
            }
            if (taskStateDict.TryGet(coroutine) == null)
                taskStateDict.Add(coroutine, new TaskState(coroutine));

            return taskStateDict.TryGet(coroutine);
        }
        /// <summary>
        /// 功能未实现
        /// </summary>
        /// <param name="coroutine"></param>
        public Coroutine CreateAndExecuteTask(IEnumerator coroutine)
        {
            TaskState taskState = CreateTask(coroutine);
            return taskState.Start();
        }
        public void StopAllTasks()
        {

        }
        public void StopTask(IEnumerator coroutine)
        {
            if (taskStateDict.TryGet(coroutine) == null)
                return;
            else
                taskStateDict.TryGet(coroutine).Stop();
        }
        private void TaskFinished(bool manual)
        {

        }

        #region UNITY_EDITOR
#if UNITY_EDITOR
        [CustomEditor(typeof(CoroutineTaskManager))]
        public class CoroutineTaskManagerEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
                CoroutineTaskManager manager = target as CoroutineTaskManager;

                if (manager == null)
                {
                    return;
                }
                if (!EditorApplication.isPlaying)
                {
                    return;
                }
                EditorGUILayout.LabelField("注册的 delayActionDict：" + manager.taskStateDict.Count.ToString());

                this.Repaint();
            }
        }
#endif
        #endregion

    }
}