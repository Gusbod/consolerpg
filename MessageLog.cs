class MessageLog
{
    private class MessageEntry
    {
        public string Message { get; set; } = "";
        public int UpdateCounter { get; set; }
    }

    public int Lines { get; private set; }
    private readonly List<MessageEntry> messageEntries = new();
    private int counter = 0;
    private int messageLifetime = 10;

    public MessageLog(int lines = 6)
    {
        Lines = lines;
    }

    public void Update()
    {
        counter++;
    }

    public void AddMessage(string message)
    {
        var splitMessage = message.Split('\n');
        foreach (var line in splitMessage)
        {
            messageEntries.Add(new MessageEntry { Message = line, UpdateCounter = counter });
        }
    }

    public IEnumerable<string> GetLastMessages()
    {
        return messageEntries
            .Where(entry => counter - entry.UpdateCounter <= messageLifetime)
            .Select(entry => entry.Message)
            .TakeLast(Lines).Reverse();
    }
}
