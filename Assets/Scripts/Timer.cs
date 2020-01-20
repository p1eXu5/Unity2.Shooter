using System;
using TMPro;

// ReSharper disable once CheckNamespace
namespace Shooter
{
    /// <summary>
    /// to make intervals between shoots for example.
    /// </summary>
    public sealed class Timer
    {
        private DateTime _start = DateTime.Now;
        private float _lastInterval = -1;
        private float _runningInterval = -1;
        private TimeSpan _elapsed;

        public Timer()
        { }

        public Timer( float runningInterval )
        {
            _lastInterval = runningInterval;
        }


        public bool IsStopped => _runningInterval < 0.0f;


        public void Start( float duration )
        {
            _start = DateTime.Now;
            _runningInterval = duration;
            _lastInterval = duration;
        }

        public void Restart()
        {
            if ( _lastInterval <= 0 ) {
                throw new InvalidOperationException( "You must set runningInterval through Start method." );
            }

            _start = DateTime.Now;
            _runningInterval = _lastInterval;
        }

        public void Update()
        {
            if ( _runningInterval > 0 ) {
                _elapsed = DateTime.Now - _start;

                if ( _elapsed.TotalSeconds > _runningInterval ) {
                    _runningInterval = 0.0f;
                    // for event check
                }
            }
            else if ( _runningInterval.Equals( 0.0f ) ) {
                _runningInterval = -1;
            }
        }

        public bool IsDingDong => _runningInterval == 0;
    }
}