namespace ChatGPTConsume.Service.Interface
{
    public interface IConsumeChatGPTService 
    {
        Task<string> PostQuestionAPI(string value);
    }
}
