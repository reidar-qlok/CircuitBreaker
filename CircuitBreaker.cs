using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitBreaker
{
    public class CircuitBreaker
    {
        private int failureThreshold;
        private int retryTimePeriod;
        private int consecutiveFailures;
        private DateTime circuitOpenTime;

        public CircuitBreaker(int failureThreshold, int retryTimePeriod)
        {
            this.failureThreshold = failureThreshold;
            this.retryTimePeriod = retryTimePeriod;
            this.consecutiveFailures = 0;
            this.circuitOpenTime = DateTime.MinValue;
        }

        public void Execute(Action action)
        {
            if (IsCircuitOpen())
            {
                if (DateTime.Now >= circuitOpenTime.AddSeconds(retryTimePeriod))
                {
                    circuitOpenTime = DateTime.MinValue;
                    consecutiveFailures = 0;
                }
                else
                {
                    throw new CircuitBreakerOpenException();
                }
            }

            try
            {
                action.Invoke();
                Reset();
            }
            catch (Exception ex)
            {
                consecutiveFailures++;
                if (consecutiveFailures >= failureThreshold)
                {
                    circuitOpenTime = DateTime.Now;
                    throw new CircuitBreakerOpenException();
                }
                else
                {
                    throw ex;
                }
            }
        }

        private bool IsCircuitOpen()
        {
            return circuitOpenTime != DateTime.MinValue;
        }

        private void Reset()
        {
            consecutiveFailures = 0;
            circuitOpenTime = DateTime.MinValue;
        }
    }
}
