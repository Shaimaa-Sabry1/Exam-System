using Exam_System.Shared.Interface;

namespace Exam_System.Shared.Helpers
{
    public class ShuffleService : IShuffleService
    {
        private readonly Random _random = new Random();
        public List<T> shuffle<T>(List<T> list)
        {
            return list.OrderBy(x => _random.Next()).ToList();
            
        }
    }
}
