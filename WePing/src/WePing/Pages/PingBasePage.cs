using MicroS_Common.Actions;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeCommon;
using WePing.Services;
using WeReduxBlazor;

namespace WePing.Pages
{
    public  abstract partial class PingBasePage: ComponentBase,IDisposable
    {
        protected virtual Task LoadDatas() => Task.CompletedTask;

        protected readonly List<IDisposable> disposables = new List<IDisposable>();
        [CascadingParameter] public ReduxDevTools<WePingState, IAction> AppState { get; set; }
        [Inject] private ILogger<PingBasePage> logger { get; set; }
        [Inject] protected IActionService ActionService { get; set; }
        protected WeQueryString QueryString => ActionService.QueryString;
        #region IDisposable 
        private bool disposedValue = false; // Pour détecter les appels redondants



        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    logger.LogDebug("Dispose");
                    // TODO: supprimer l'état managé (objets managés).
                    foreach (var item in disposables)
                    {
                        item.Dispose();
                    }
                }

                // TODO: libérer les ressources non managées (objets non managés) et remplacer un finaliseur ci-dessous.
                // TODO: définir les champs de grande taille avec la valeur Null.

                disposedValue = true;
            }
        }

        // TODO: remplacer un finaliseur seulement si la fonction Dispose(bool disposing) ci-dessus a du code pour libérer les ressources non managées.
        // ~PingBasePage()
        // {
        //   // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
        //   Dispose(false);
        // }

        // Ce code est ajouté pour implémenter correctement le modèle supprimable.
        public void Dispose()
        {
            // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
            Dispose(true);
            // TODO: supprimer les marques de commentaire pour la ligne suivante si le finaliseur est remplacé ci-dessus.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
