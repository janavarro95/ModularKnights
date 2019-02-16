using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Enums
    {

        /// <summary>
        /// The operating system that the game is run on.
        /// </summary>
        public enum OperatingSystem
        {
            Windows,
            Mac,
            Linux,
            PS3,
            PS4,
            XBoxOne,
            NintendoSwitch,
            Android,
            IPhone,
            Other
        }

        /// <summary>
        /// The state that says where we are in the game.
        /// </summary>
        public enum GameState
        {
            MainMenu,
            GamePlay,
            Credits
        }

        public enum FacingDirection
        {
            Up,
            Left,
            Down,
            Right
        }

        /// <summary>
        /// Deals with all of the states a timer could be in.
        /// </summary>
        public enum TimerState
        {
            /// <summary>
            /// The timer has been initialized but is not ticking.
            /// </summary>
            Initialized,
            /// <summary>
            /// The timer has started ticking.
            /// </summary>
            Ticking,
            /// <summary>
            /// The timer has finished.
            /// </summary>
            Finished,
            /// <summary>
            /// The timer has stopped.
            /// </summary>
            Stopped,
            /// <summary>
            /// The timer has paused.
            /// </summary>
            Paused
        }

        public enum TimerType
        {
            CountDown,
            CountUp
        }

        public enum Visibility
        {
            Visible,
            Invisible
        }

        /// <summary>
        /// Gets all of the values stored in an enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] GetValues<T>()
        {
            return (T[])Enum.GetValues(typeof(T));
        }

    }
}
