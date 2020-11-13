using System;
using System.Reflection;

namespace Assign3_Sasha_Srinivas_n01458354.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}