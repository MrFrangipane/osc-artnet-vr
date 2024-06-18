using System;

namespace Scripts
{
    public class MovingHeadRotation
    {
        public float ValueDifferenceThreshold = 10f;
        public float SpeedDifferenceThreshold = 2f;
        public float SpeedStep = .5f;
        public float NominalSpeed = 5f;
        
        private float _targetValue = 0f;
        private float _actualValue = 0f;

        private float _targetSpeed = 0f;
        private float _actualSpeed = 0f;

        public void SetTargetValue(float target)
        {
            _targetValue = target;
        }

        public float Update()
        {
            // 
            // Speed
            var valueDifference = _targetValue - _actualValue;
            if (valueDifference > ValueDifferenceThreshold)
            {
                _targetSpeed = NominalSpeed;
            }
            else if (valueDifference < -ValueDifferenceThreshold)
            {
                _targetSpeed = -NominalSpeed;
            }

            var speedDifference = _targetSpeed - _actualSpeed;
            if (speedDifference > SpeedDifferenceThreshold)
            {
                _actualSpeed += SpeedStep;
            }
            else if (speedDifference < -SpeedDifferenceThreshold)
            {
                _actualSpeed -= SpeedStep;
            }
            
            // Speed OK
            if (Math.Abs(_targetSpeed - _actualSpeed) < SpeedDifferenceThreshold)
            {
                _actualSpeed = _targetSpeed;
            }
            
            //
            // Value
            _actualValue += _actualSpeed;
            if (Math.Abs(_targetValue - _actualValue) < ValueDifferenceThreshold)
            {
                _actualValue = _targetValue;
            }
            
            return _actualValue;
        }
    }
}