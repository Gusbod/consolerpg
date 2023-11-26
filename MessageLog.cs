class MessageLog
{
    private Queue<string> messages = new Queue<string>();

    public void AddMessage(string message)
    {
        messages.Enqueue(message);

        if (messages.Count > 1) // Limit the number of messages stored
        {
            messages.Dequeue();
        }
    }

    public IEnumerable<string> GetMessages()
    {
        return messages;
    }
}