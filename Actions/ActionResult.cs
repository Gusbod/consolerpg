class ActionResult
{
    public bool Success { get; private set; } = true;
    public string Message { get; private set; } = "";

    public ActionResult(bool success = true, string message = "")
    {
        Message = message;
        Success = success;
    }
}
