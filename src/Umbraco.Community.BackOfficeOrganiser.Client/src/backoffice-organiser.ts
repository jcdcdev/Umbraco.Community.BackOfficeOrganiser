import {customElement, html, LitElement, property} from "lit-element";

enum OrganiseType {
    Unknown = 0,
    ContentTypes = 1,
    MediaTypes = 2,
    MemberTypes = 3,
    DataTypes = 4,
    All = 99
}

@customElement('backoffice-organiser')
export default class MyElement extends LitElement {

    @property()
    types: string[];

    @property()
    selectedType: OrganiseType = OrganiseType.Unknown;

    @property()
    loading: boolean = false;

    @property()
    confirmRequired: boolean = false;

    @property()
    modal: any = html``;

    response: any = null;

    constructor() {
        super();
        this.types = Object.keys(OrganiseType).filter((v) => isNaN(Number(v)) && v !== 'Unknown')
    }

    render() {
        const toasts = [];
        let modal = null;
        if (this.confirmRequired) {
            modal = this.renderConfirm();
        }

        if (this.response !== null) {
            const message = this.response.message;
            const success = !this.response.error;
            const headline = success ? "Success" : "Error";
            const toast = this.renderToast(headline, message, success);
            toasts.push(toast);
        }

        const options = this.types.map(type => html`
            <uui-combobox-list-option style="padding: 10px" value="${type}">${type}</uui-combobox-list-option>`)

        const disableButton = this.selectedType === OrganiseType.Unknown;
        const form = html`
            <uui-form>
                <form id="backoffice-organiser-form" @submit=${this.onSubmit} name="backofficeOrganiserForm">
                    <uui-form-layout-item>
                        <uui-label for="SelectedType" slot="label" required="">Select type to organise</uui-label>
                        <uui-combobox-list id="SelectedType" name="SelectedType" @change=${this.onChange}
                                           value=${this.selectedType.toString()}>
                            ${options}
                        </uui-combobox-list>
                    </uui-form-layout-item>
                    <uui-button type="submit"
                                look="primary"
                                label="Submit"
                                .disabled="${disableButton}"></uui-button>
                </form>
            </uui-form>

            <uui-toast-notification-container
                    id="toastContainer"
                    auto-close="3000"
                    bottom-up=""
                    style="top:0; left:0; right:0; height: 100vh; padding: var(--uui-size-layout-1);">
                ${toasts}
            </uui-toast-notification-container>
        `;
        const loader = html`
            <uui-loader-bar style="color: blue"></uui-loader-bar>
        `;
        // noinspection CssUnresolvedCustomProperty
        return html`
            <uui-box headline="Back Office Organiser">
                ${this.loading ? loader : form}
            </uui-box>

            <uui-modal-container>
                ${modal}
            </uui-modal-container>
        `;
    }

    onChange = (event: any) => {
        const val = event.currentTarget.value;
        this.selectedType = this.stringToEnum(val.toString())
    };

    onSubmit = (e: Event) => {
        e.preventDefault();
        debugger;
        const form = e.currentTarget as HTMLFormElement;
        const isValid = form.checkValidity();
        if (!isValid) {
            return;
        }

        this.confirmRequired = true;
    };

    organiseType = async () => {
        this.confirmRequired = false;
        this.loading = true;
        // @ts-ignore
        const umbracoPath = window.Umbraco.Sys.ServerVariables.umbracoSettings.umbracoPath;
        const response = await fetch(`${umbracoPath}/backoffice/api/BackOfficeOrganiser/Organise?type=${this.selectedType}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });

        const data = await response.json();
        this.loading = false;
        this.response = data;
    };

    stringToEnum = (value: string): OrganiseType => {
        if (Object.values(OrganiseType).includes(value)) {
            return value as unknown as OrganiseType;
        }
        return OrganiseType.All;
    };

    renderConfirm = () => html`
        <uui-modal-dialog @close=${this.cancelOrganise}>
            <uui-dialog>
                <uui-dialog-layout headline="Confirm Organise">
                    <p>
                        Organise <b>${this.selectedType}</b>?<br><br>This cannot be undone!
                    </p>
                    <uui-button slot="actions"
                                @click="${this.cancelOrganise}">Cancel
                    </uui-button>
                    <uui-button slot="actions"
                                @click="${() => this.organiseType()}"
                                look="primary"
                                color="positive">Confirm
                    </uui-button>
                </uui-dialog-layout>
            </uui-dialog>
        </uui-modal-dialog>
    `;

    private renderToast(headline: string, message: string, success: boolean = true) {
        const color = success ? "success" : "danger";
        return html`
            <uui-toast-notification name=${headline} color=${color}>
                <h3>${headline}</h3>
                <p>${message}</p>
            </uui-toast-notification>
        `;
    }

    private cancelOrganise() {
        this.confirmRequired = false
    }
}

declare global {
    interface HTMLElementTagNameMap {
        'backoffice-organiser': MyElement;
    }
}