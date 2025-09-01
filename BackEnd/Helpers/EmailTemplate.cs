namespace BackEnd.Helpers
{
    public class EmailTemplate
    {
        private readonly string _templatePath;

        public EmailTemplate(IWebHostEnvironment env)
        {
            _templatePath = Path.Combine(env.ContentRootPath, "EmailTemplates");
        }

        public string GetTemplate(string templateName, Dictionary<string, string> placeholders)
        {
            var filePath = Path.Combine(_templatePath, templateName);
            var html = File.ReadAllText(filePath);

            foreach (var placeholder in placeholders)
            {
                html = html.Replace("{{" + placeholder.Key + "}}", placeholder.Value);
            }

            return html;
        }
    }
}
