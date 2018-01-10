using System.Threading.Tasks;
using Don2018.EventCloud.Configuration.Dto;

namespace Don2018.EventCloud.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
