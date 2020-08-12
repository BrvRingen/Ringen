


namespace Ringen
{
	public enum ResourcesEnum
	{
		Ringen_DictMainForm,
	}

	public class TranslateExtension : Ringen.Core.TranslationManager.TranslateExtension
	{
		#region constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="TranslateExtension"/> class.
		/// </summary>
		/// <param name="key">The key.</param>
		public TranslateExtension(DictMainForm key)
		{
			Key = key.ToString();
			Resource = ResourcesEnum.Ringen_DictMainForm.ToString();
		}
		#endregion constructors
	}

	public enum DictMainForm
	{
		/// <summary>Info</summary>
		CustomWindowInfo,
		/// <summary>Hilfe</summary>
		CustomWindowHelp,
		/// <summary>Schließen</summary>
		Close,
		/// <summary>Close</summary>
		CloseRingen,
		/// <summary>Ringen</summary>
		MainFormTitle,
		/// <summary>Benutzername</summary>
		Username,
		/// <summary>Passwort</summary>
		Password,
		/// <summary>OK</summary>
		OK,
		/// <summary>Abbrechen</summary>
		Cancel,
		/// <summary>Domain</summary>
		UserDomain,
	}

}

