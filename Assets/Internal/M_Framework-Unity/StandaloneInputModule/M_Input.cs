using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine
{
    public class M_Input : MonoBehaviour
    {
        private bool singleClick;
        private bool doubleClick;

        private static bool[] ars = new bool[3];
        private static bool[] arsThree0 = new bool[3];
        private static bool[] arsLongP = new bool[3];


        public static bool GetDoubleMouseButtonClick(int index)
        {
            bool result = false;
            if (ars[index])
            {
                result = true;
                ars[index] = false;
            }

            return result;
        }

        public static bool GetThreeMouse0ButtonClick()
        {
            bool result = false;
            if (!arsThree0[0] && !arsThree0[1] && arsThree0[2])
            {
                result = true;
                arsThree0[0] = arsThree0[1] = arsThree0[2] = false;
            }

            return result;
        }

        public static bool GetLongPressMouseButton(int index)
        {
            bool result = false;
            if (arsLongP[index])
            {
                result = true;
                arsLongP[index] = false;
            }

            return result;
        }

        private void Update()
        {
            ExcuteMouse0();
            ExcuteMouse1();
            ExcuteMouse2();

            ExcuteThreeMouse0();

            ExcuteLongPressMouse0();
            ExcuteLongPressMouse1();
            ExcuteLongPressMouse2();

            Tag();
        }

        private void Tag()
        {
        }

        [SerializeField] private short _intervalThreeCount = 70;
        [SerializeField] private short _countThree = 0;
        private void ExcuteThreeMouse0()
        {
            _countThree--;

            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                if (_countThree > 0 && _countThree < _intervalThreeCount / 2)
                {
                    if (arsThree0[1])
                    {
                        arsThree0[2] = true;
                        arsThree0[1] = false;
                        arsThree0[0] = false;
                    }
                }
                else if (_countThree > _intervalThreeCount / 2)
                {
                    _countThree = (short)(_intervalThreeCount / 2);
                    if (arsThree0[0])
                    {
                        arsThree0[1] = true;
                        arsThree0[0] = false;
                    }
                }
                else
                {
                    _countThree = _intervalThreeCount;
                    arsThree0[0] = true;
                }
            }

            if (_countThree < 0)
            {
                _countThree = 0;
            }
        }

        [SerializeField] private short _intervalCount = 35;
        private short _count = 0;
        private void ExcuteMouse0()
        {
            _count--;

            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                if (_count > 0)
                {
                    ars[0] = true;
                }
                _count = _intervalCount;
            }

            if (_count < 0)
            {
                _count = 0;
            }
        }

        private short _interval1Count = 35;
        private short _count1 = 0;
        private void ExcuteMouse1()
        {
            _count1--;

            if (UnityEngine.Input.GetMouseButtonDown(1))
            {
                if (_count1 > 0)
                {
                    ars[1] = true;
                }
                _count1 = _interval1Count;
            }

            if (_count1 < 0)
            {
                _count1 = 0;
            }
        }

        private short _interval2Count = 35;
        private short _count2 = 0;
        private void ExcuteMouse2()
        {
            _count2--;

            if (UnityEngine.Input.GetMouseButtonDown(2))
            {
                if (_count2 > 0)
                {
                    ars[2] = true;
                }
                _count2 = _interval2Count;
            }

            if (_count2 < 0)
            {
                _count2 = 0;
            }
        }

        [SerializeField] private short _intervalLongPressCount = 120;
        private short _countLongPress = 0;
        private void ExcuteLongPressMouse0()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                _countLongPress = 0;
            }

            if (UnityEngine.Input.GetMouseButton(0))
            {
                _countLongPress++;

                if (_countLongPress == _intervalLongPressCount)
                {
                    arsLongP[0] = true;
                    _countLongPress++;
                }
            }

            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                _countLongPress = 0;
            }

            if (_countLongPress >= _intervalLongPressCount)
            {
                _countLongPress = (short)(_intervalLongPressCount + 1);
            }
        }

        private short _intervalLongPress1Count = 120;
        private short _countLongPress1 = 0;
        private void ExcuteLongPressMouse1()
        {
            if (UnityEngine.Input.GetMouseButtonDown(1))
            {
                _countLongPress1 = 0;
            }

            if (UnityEngine.Input.GetMouseButton(1))
            {
                _countLongPress1++;

                if (_countLongPress1 == _intervalLongPress1Count)
                {
                    arsLongP[1] = true;
                    _countLongPress1++;
                }
            }

            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                _countLongPress1 = 0;
            }

            if (_countLongPress1 >= _intervalLongPress1Count)
            {
                _countLongPress1 = (short)(_intervalLongPress1Count + 1);
            }
        }

        private short _intervalLongPress2Count = 120;
        private short _countLongPress2 = 0;
        private void ExcuteLongPressMouse2()
        {
            if (UnityEngine.Input.GetMouseButtonDown(2))
            {
                _countLongPress2 = 0;
            }

            if (UnityEngine.Input.GetMouseButton(2))
            {
                _countLongPress2++;

                if (_countLongPress2 == _intervalLongPress2Count)
                {
                    arsLongP[2] = true;
                    _countLongPress2++;
                }
            }

            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                _countLongPress2 = 0;
            }

            if (_countLongPress2 >= _intervalLongPress2Count)
            {
                _countLongPress2 = (short)(_intervalLongPress2Count + 1);
            }
        }

    }
}
