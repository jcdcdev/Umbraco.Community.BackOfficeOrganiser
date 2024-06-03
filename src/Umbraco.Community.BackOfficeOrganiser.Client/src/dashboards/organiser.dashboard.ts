import {customElement, property, state} from "lit/decorators.js";
import {css, html, LitElement} from "lit";
import {UmbElementMixin} from "@umbraco-cms/backoffice/element-api";
import {UMB_NOTIFICATION_CONTEXT, UmbNotificationContext} from "@umbraco-cms/backoffice/notification";
import {BackofficeOrganiserContext} from "../context/organiser.context.ts";
import {OrganiseTypeModel} from "../models/organise-type-model.ts";
import {OrganiseType} from "../models/organise-type.ts";

@customElement('backoffice-organiser')
export default class BackofficeOrganiser extends UmbElementMixin(LitElement) {

	@property()
	loading: boolean = false;

	#backofficeOrganiserContext?: BackofficeOrganiserContext;
	#notificationContext?: UmbNotificationContext;

	@property()
	confirmRequired: boolean = false;

	@state()
	types: OrganiseTypeModel[] = [
		{
			value: 1,
			label: "Content Types",
			description: "Organise content types",
			selected: false,
		},
		{
			value: 2,
			label: "Media Types",
			description: "Organise media types",
			selected: false,
		},
		{
			value: 3,
			label: "Member Types",
			description: "Organise member types",
			selected: false,
		},
		{
			value: 4,
			label: "Data Types",
			description: "Organise data types",
			selected: false,
		}
	];

	// @state()
	// toast: Toast | null = null;
	private contentTypes: boolean = false;
	private mediaTypes: boolean = false;
	private memberTypes: boolean = false;
	private dataTypes: boolean = false;

	constructor() {
		super();

		this.#backofficeOrganiserContext = new BackofficeOrganiserContext(this);

		this.consumeContext(UMB_NOTIFICATION_CONTEXT, (context) => {
			this.#notificationContext = context;
		});
	}

	render() {
		let modal = null;
		if (this.confirmRequired) {
			modal = this.renderConfirm();
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
		`;
		const loader = html`
			<uui-loader-bar style="color: blue"></uui-loader-bar>
		`;

		return html`
			<div class="dashboard">

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
			</div>
		`;
	}

	private toggleType(type: OrganiseTypeModel) {
		type.selected = !type.selected;
		switch (type.value) {
			case OrganiseType.ContentTypes:
				this.contentTypes = type.selected;
				break;
			case OrganiseType.MediaTypes:
				this.mediaTypes = type.selected;
				break;
			case OrganiseType.MemberTypes:
				this.memberTypes = type.selected;
				break;
			case OrganiseType.DataTypes:
				this.dataTypes = type.selected;
				break;
		}
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
		const request = {
			requestBody: {
				contentTypes: this.contentTypes,
				mediaTypes: this.mediaTypes,
				memberTypes: this.memberTypes,
				dataTypes: this.dataTypes
			}
		};

		const response = await this.#backofficeOrganiserContext?.organise(request);
		const data = response?.data;
		const success = !data?.error ?? false;
		const heading = success ? "Success" : "Error";
		const color = success ? "positive" : "danger";
		this.#notificationContext?.peek(color, {
			data: {
				message: data?.message ?? "",
				headline: heading
			}
		});

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

	private cancelOrganise() {
		this.confirmRequired = false
	}

	static styles = [
		css`
			.dashboard{
				padding:24px;
			}
			
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