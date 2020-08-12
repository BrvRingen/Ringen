


namespace Ringen.Plugin.CsView
{
	public enum ResourcesEnum
	{
		Ringen_Plugin_CsView_DictPluginMain,
	}

	public class TranslateExtension : Ringen.Core.TranslationManager.TranslateExtension
	{
		#region constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="TranslateExtension"/> class.
		/// </summary>
		/// <param name="key">The key.</param>
		public TranslateExtension(DictPluginMain key)
		{
			Key = key.ToString();
			Resource = ResourcesEnum.Ringen_Plugin_CsView_DictPluginMain.ToString();
		}
		#endregion constructors
	}

	public enum DictPluginMain
	{
		/// <summary>Heimmannschaft</summary>
		HomeTeamName,
		/// <summary>Gastmannschaft</summary>
		OpponentTeamName,
		/// <summary>Punkte Heim</summary>
		HomePoints,
		/// <summary>Punkte Gast</summary>
		OpponentPoints,
		/// <summary>Datum</summary>
		BoutDate,
		/// <summary>Anzahl Zuschauer</summary>
		Audience,
		/// <summary>Veranstalltungsort</summary>
		Location,
		/// <summary>Name des Bearbeiters</summary>
		EditorName,
		/// <summary>Kommentar</summary>
		EditorComment,
		/// <summary>Schiedsrichter</summary>
		Referee,
		/// <summary>Mannschaftskampf</summary>
		Competition,
		/// <summary>
		/// Unbesetzt muss nicht mehr eingetragen werden. Dies wird dem Protokoll entnommen.
		/// Ist im Feld Schiedsrichter nichts vermerkt, so ist das im Kommentar zu vermerken.
		/// Verspäteter Start und die Pause müssen nicht mehr eingetragen werden. Diese werden den Kämpfen entnommen.
		/// </summary>
		EditorCommentHint,
	}

}

