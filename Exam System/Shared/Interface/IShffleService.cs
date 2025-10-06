namespace Exam_System.Shared.Interface
{
    public interface IShuffleService
    {
        List<T> shuffle<T>(List<T> list);
    }
}
