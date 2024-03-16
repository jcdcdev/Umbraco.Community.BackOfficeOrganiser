// noinspection CssUnresolvedCustomProperty

import {css, customElement, html, LitElement, property, state} from "lit-element";
import {OrganiseType} from "./organise-type.ts";
import {Toast} from "./toast.ts";

@customElement('backoffice-organiser')
export default class BackofficeOrganiser extends LitElement {

	@property()
	loading: boolean = false;

	@property()
	confirmRequired: boolean = false;

	@state()
	types: OrganiseType[] = [
		{
			value: 1,
			label: "Content Types",
			description: "Organise content types",
			selected: false
		},
		{
			value: 2,
			label: "Media Types",
			description: "Organise media types",
			selected: false
		},
		{
			value: 3,
			label: "Member Types",
			description: "Organise member types",
			selected: false
		},
		{
			value: 4,
			label: "Data Types",
			description: "Organise data types",
			selected: false
		}
	];

	@state()
	toast: Toast | null = null;

	// @ts-ignore
	private umbracoPath = window.Umbraco.Sys.ServerVariables.umbracoSettings.umbracoPath;
	private apiUrl = `${this.umbracoPath}/backoffice/api/BackOfficeOrganiser/Organise`;

	render() {
		const toasts = [];
		let modal = null;
		if (this.confirmRequired) {
			modal = this.renderConfirm();
		}

		if (this.toast !== null) {
			const toast = this.renderToast(this.toast);
			toasts.push(toast);
		}

		const organiseTypes = this.types.map(type => {
			const look = type.selected ? "primary" : "placeholder";
			return html
				`
                    <uui-button @click="${() => this.toggleType(type)}" style="--uui-button-height: 200px" look="${look}">
                        ${type.label}
                    </uui-button>
				`;
		})

		const disableButton = this.types.filter(x => x.selected).length === 0;
		const form = html`
            <uui-form>
                <form id="backoffice-organiser-form" @submit=${this.onSubmit} name="backofficeOrganiserForm">
                    <uui-form-layout-item>
                        <uui-label slot="label" for="parent" required="">Select types</uui-label>
                        <span slot="description">
							Select the types to organise
						</span>
                        <div class="organise-type-container">
                            ${organiseTypes}
                        </div>
                    </uui-form-layout-item>
                    <uui-button type="submit" look="primary" label="Submit" .disabled="${disableButton}"></uui-button>
                </form>
            </uui-form>

            <uui-toast-notification-container
                    class="toast-container"
                    id="toastContainer"
                    auto-close="3000"
                    bottom-up="">
                ${toasts}
            </uui-toast-notification-container>
        `;
		const loader = html`
			<uui-loader-bar style="color: blue"></uui-loader-bar>
		`;

		return html`
            <uui-box headline="Welcome">
                <p>
                    This dashboard is designed to help you organise your Document Types, Media Types, Member Types and Data Types.
                </p>
                <p>
                    To get started, select at least one type to organise and click the submit button.
                </p>
                <uui-icon-registry-essential>
                    <uui-button look="outline"
                                href="https://github.com/jcdcdev/Umbraco.Community.BackOfficeOrganiser/?tab=readme-ov-file#umbracocommunitybackofficeorganiser"
                                target="_blank">
                        <uui-icon name="document"></uui-icon>
                        Documentation
                    </uui-button>

                    <uui-button look="outline"
                                href="https://github.com/jcdcdev/Umbraco.Community.BackOfficeOrganiser/issues/new?assignees=bug&template=bug.yml"
                                target="_blank">
                        <uui-icon name="alert"></uui-icon>
                        Report a Bug
                    </uui-button>

                    <uui-button look="outline"
                                href="https://github.com/jcdcdev/Umbraco.Community.BackOfficeOrganiser/issues/new?assignees=enhancement&template=feature_request.yml"
                                target="_blank">
                        <uui-icon name="wand"></uui-icon>
                        Request a Feature
                    </uui-button>
                </uui-icon-registry-essential>

            </uui-box>
            <br>
            <uui-box headline="Organise">
                ${this.loading ? loader : form}
            </uui-box>
            <uui-modal-container>
                ${modal}
            </uui-modal-container>
		`;
	}

	private toggleType(type: OrganiseType) {
		type.selected = !type.selected;
		this.requestUpdate();
	}

	private onSubmit = (e: Event) => {
		e.preventDefault();
		const form = e.currentTarget as HTMLFormElement;
		const isValid = form.checkValidity();
		if (!isValid) {
			return;
		}

		this.confirmRequired = true;
	};

	private confirmOrganise = async () => {
		this.confirmRequired = false;
		this.loading = true;

		const response = await fetch(this.apiUrl, {
			method: 'POST',
			body: JSON.stringify({
				types: this.types.filter(x => x.selected).map(x => x.value)
			}),
			headers: {
				'Content-Type': 'application/json'
			}
		});

		const data = await response.json();
		const success = !data.error;
		const heading = success ? "Success" : "Error";
		this.toast = new Toast(heading, data.message, success);
		this.loading = false;
	};

	renderConfirm = () => html`
		<uui-modal-dialog @close=${this.cancelOrganise}>
			<uui-dialog>
				<uui-dialog-layout headline="Confirm Organise">
					<p>
						The following types will be organised:
					</p>
					<ul>
						${this.types.filter(x => x.selected).map(x =>
							html`
								<li>${x.label}</li>
							`)}
					</ul>
					<div class="alert">
						<span>
                        This cannot be undone!
						</span>
					</div>
					<uui-button slot="actions"
								@click="${this.cancelOrganise}">Cancel
					</uui-button>
					<uui-button slot="actions"
								@click="${this.confirmOrganise}"
								look="primary">Confirm
					</uui-button>
				</uui-dialog-layout>
			</uui-dialog>
		</uui-modal-dialog>
	`;

	private renderToast(toast: Toast) {
		const message = toast.message;
		const success = toast.success;
		const headline = toast.headline;
		const color = success ? "positive" : "danger";
		return html
			`
				<uui-toast-notification name=${headline} color=${color}>
					<h3>${headline}</h3>
					<p>${message}</p>
				</uui-toast-notification>
			`;
	}

	private cancelOrganise() {
		this.confirmRequired = false
	}

	static styles = [
		css`
          .organise-type-container uui-button {
            width: 100%;
          }

          .toast-container {
            top: 0;
            left: 0;
            right: 0;
            height: 100vh;
            padding: var(--uui-size-layout-1);
          }

          .organise-type-container {
            display: flex;
            flex-direction: row;
            gap: var(--uui-size-3);
            max-width: 900px;
          }

          .organise-type {
            background-color: var(--uui-color-background);
            cursor: pointer;
            padding: var(--uui-size-6);
          }

          .organise-type.active {
            background-color: var(--uui-color-selected);
            color: white;
          }

          .alert {
            padding: var(--uui-size-3);
            background-color: var(--uui-color-danger-emphasis);
            color: var(--uui-color-danger-contrast);
          }
		`
	]
}

declare global {
	interface HTMLElementTagNameMap {
		'backoffice-organiser': BackofficeOrganiser;
	}
}