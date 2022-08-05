
using System;
using System.Collections.Generic;
using System.Text;

namespace Tempus.Models
{                                                               
    public class StopwatchModel
    {
        #region StopwatchModel Class Properties
            
        private DateTime _startedTime;
        private bool _paused;
        private DateTime _pausedAt;
        private TimeSpan _totalPausedTime;        
    
        #endregion StopwatchModel Class Properties

        #region StopwatchModel Class Constructor

        public StopwatchModel() => Reset();

        #endregion StopwatchModel Class Constructor
    
        #region StopwatchModel Class Implementation

        public bool Running
        {
            get => (_startedTime != DateTime.MinValue) && !_paused;

            set
            {
                if (value)
                {
                    _paused = false;

                    if (_pausedAt != DateTime.MinValue)
                    {
                        _totalPausedTime += DateTime.Now - _pausedAt;
                    }

                    if (_startedTime == DateTime.MinValue)
                    {
                        _startedTime = DateTime.Now;
                    }
                }

                else
                {
                    _paused = true;
                    _pausedAt = DateTime.Now;
                }
            }
        }

        public DateTime StartTime => _startedTime; 

        public TimeSpan Elapsed => _paused ? _pausedAt - _startedTime - _totalPausedTime
                                   : _startedTime != DateTime.MinValue ? DateTime.Now - _startedTime - _totalPausedTime 
                                   : TimeSpan.Zero;

        public void Reset()
        {
            _startedTime = DateTime.MinValue;   
            _pausedAt = DateTime.MinValue;
            _paused = false;
            _totalPausedTime = TimeSpan.Zero;
        }
            
        #endregion StopwatchModel Class Implementation
    }
}
