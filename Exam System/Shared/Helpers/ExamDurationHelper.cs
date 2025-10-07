using Exam_System.Domain.Entities;

namespace Exam_System.Shared.Helpers
{
    public static class ExamDurationHelper
    {
        /// <summary>
        /// Checks if the exam duration has expired based on attempt start time
        /// </summary>
        /// <param name="attempt">The exam attempt</param>
        /// <param name="exam">The exam entity</param>
        /// <returns>True if time has expired, false otherwise</returns>
        public static bool IsExamTimeExpired(attembt attempt, Exam exam)
        {
            if (attempt == null || exam == null)
                return false;

            var elapsedTime = DateTime.Now - attempt.startTime;
            var examDuration = TimeSpan.FromMinutes(exam.DurationInMinutes);

            return elapsedTime > examDuration;
        }

        /// <summary>
        /// Gets the remaining time for an exam attempt
        /// </summary>
        /// <param name="attempt">The exam attempt</param>
        /// <param name="exam">The exam entity</param>
        /// <returns>TimeSpan representing remaining time, or TimeSpan.Zero if expired</returns>
        public static TimeSpan GetRemainingTime(attembt attempt, Exam exam)
        {
            if (attempt == null || exam == null)
                return TimeSpan.Zero;

            var elapsedTime = DateTime.Now - attempt.startTime;
            var examDuration = TimeSpan.FromMinutes(exam.DurationInMinutes);
            var remainingTime = examDuration - elapsedTime;

            return remainingTime > TimeSpan.Zero ? remainingTime : TimeSpan.Zero;
        }

        /// <summary>
        /// Checks if exam is currently active (within start and end dates)
        /// </summary>
        /// <param name="exam">The exam entity</param>
        /// <returns>True if exam is active, false otherwise</returns>
        public static bool IsExamActive(Exam exam)
        {
            if (exam == null)
                return false;

            var now = DateTime.Now;
            return exam.StartDate <= now && exam.EndDate >= now;
        }
    }
}

