namespace VisualEmbed.ProjectSupport;

public interface ISupportUI
{
	void ShowMsg(string Msg, params object[] Args);

	void ShowError(string Msg, params object[] Args);

	void ShowWarning(string Msg, params object[] Args);

	int Select(string Msg, string[] selectList);
}
