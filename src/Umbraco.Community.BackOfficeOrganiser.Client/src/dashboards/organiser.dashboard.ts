import {customElement, state} from "lit/decorators.js";
import {css, html, LitElement} from "lit";
import {UmbElementMixin} from "@umbraco-cms/backoffice/element-api";
import {UMB_NOTIFICATION_CONTEXT, UmbNotificationContext} from "@umbraco-cms/backoffice/notification";
import {BACKOFFICE_ORGANISER_CONTEXT_TOKEN, BackofficeOrganiserContext} from "../context/organiser.context.ts";
import {OrganiseTypeModel} from "../models/organise-type-model.ts";
import {OrganiseType} from "../models/organise-type.ts";
import {UMB_CONFIRM_MODAL, UMB_MODAL_MANAGER_CONTEXT, UmbModalManagerContext} from "@umbraco-cms/backoffice/modal";
import {OrganiseInfoModel, OrganiseInfoResponse} from "../api";

@customElement('backoffice-organiser')
export default class BackofficeOrganiser extends UmbElementMixin(LitElement) {

	@state()
	loading: boolean = false;
	@state()
	contentTypes: boolean = false;
	@state()
	mediaTypes: boolean = false;
	@state()
	memberTypes: boolean = false;
	@state()
	dataTypes: boolean = false;
	@state()
	info?: OrganiseInfoResponse;
	@state()
	types: OrganiseTypeModel[] = [
		{
			value: 1,
			label: this.localize.term("boo_contentTypes"),
			selected: false,
		},
		{
			value: 2,
			label: this.localize.term("boo_mediaTypes"),
			selected: false,
		},
		{
			value: 3,
			label: this.localize.term("boo_memberTypes"),
			selected: false,
		},
		{
			value: 4,
			label: this.localize.term("boo_dataTypes"),
			selected: false,
		}
	];

	#modalManagerContext?: UmbModalManagerContext;
	#backofficeOrganiserContext?: BackofficeOrganiserContext;
	#notificationContext?: UmbNotificationContext;

	constructor() {
		super();
		this.consumeContext(BACKOFFICE_ORGANISER_CONTEXT_TOKEN, async (context) => {
			this.#backofficeOrganiserContext = context;
			const data = await context?.getInfo();
			if (!data?.error && data?.data) {
				this.info = data.data;
			}
		});

		this.consumeContext(UMB_MODAL_MANAGER_CONTEXT, (context) => {
			this.#modalManagerContext = context;
		});

		this.consumeContext(UMB_NOTIFICATION_CONTEXT, (context) => {
			this.#notificationContext = context;
		});
	}

	_showInfoModal(type: OrganiseTypeModel) {
		const content = this.renderModal(type);
		const modalContext = this.#modalManagerContext?.open(
			this, UMB_CONFIRM_MODAL,
			{
				data: {
					headline: type.label,
					content: content,
					color: "positive",
					confirmLabel: this.localize.term("general_close"),
					cancelLabel: " "
				}
			}
		);

		modalContext
			?.onSubmit()
			.then(() => {
			})
			.catch(() => {
			});
	}

	renderModal(type: OrganiseTypeModel) {
		const items = this._getItems(type.value);

		const organiseActions = items.map((x, i) => html`
			<div>
				<h3>${i + 1}. ${x.name}</h3>
				<p>${x.description}</p>
			</div>
		`);

		return html
			`
				<p>${this.localize.term("boo_organiseActionInfoIntroduction")}</p>
				${organiseActions}
			`;
	}

	render() {
		const organiseTypes = this.types.map((type) => {
			const look = type.selected ? "primary" : "outline";
			const icon = type.selected ? "check" : "remove";
			const text = type.selected ? this.localize.term("boo_selected") : this.localize.term("boo_disabled");
			const label = type.label;
			const count = this._getItems(type.value).length;
			return html
				`
					<div class="organiser-type">
						<h3>${type.label}</h3>
						<p>
							<uui-button
								look="placeholder"
								label="${this.localize.term("general_info")}"
								color="info"
								@click="${() => this._showInfoModal(type)}">
								<uui-icon name="info"></uui-icon>
								${this.localize.term("boo_numberOfOrganisers", count)}
							</uui-button>
						</p>
						<uui-button label="${label}" @click="${() => this._toggleType(type)}" look="${look}">
							<uui-icon name="${icon}"></uui-icon>
							${text}
						</uui-button>
					</div>
				`;
		})

		const disableButton = this.types.filter(x => x.selected).length === 0;
		const form = html`
			<uui-form>
				<form id="backoffice-organiser-form" @submit=${this._onSubmit} name="backofficeOrganiserForm">
					<uui-form-layout-item>
						<uui-label slot="label" for="parent" required="">${this.localize.term("boo_selectTypesLabel")}</uui-label>
						<span slot="description">
							${this.localize.term("boo_selectTypes")}
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
				<uui-box headline="${this.localize.term("boo_title")}">
					<p>
						${this.localize.term("boo_introduction")}
					</p>
					<uui-button-group>
						<uui-button look="outline"
									href="https://github.com/jcdcdev/Umbraco.Community.BackOfficeOrganiser/?tab=readme-ov-file#umbracocommunitybackofficeorganiser"
									target="_blank">
							<uui-icon name="document"></uui-icon>
							${this.localize.term("boo_documentation")}
						</uui-button>
						<uui-button look="outline"
									href="https://github.com/jcdcdev/Umbraco.Community.BackOfficeOrganiser/issues/new?assignees=bug&template=bug.yml"
									target="_blank">
							<uui-icon name="alert"></uui-icon>
							${this.localize.term("boo_reportBug")}
						</uui-button>
						<uui-button look="outline"
									href="https://github.com/jcdcdev/Umbraco.Community.BackOfficeOrganiser/issues/new?assignees=enhancement&template=feature_request.yml"
									target="_blank">
							<uui-icon name="wand"></uui-icon>
							${this.localize.term("boo_requestFeature")}
						</uui-button>
					</uui-button-group>
				</uui-box>
				<br>
				<uui-box headline="${this.localize.term("boo_organise")}">
					${this.loading ? loader : form}
				</uui-box>
			</div>
		`;
	}

	_toggleType(type: OrganiseTypeModel) {
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

	_onSubmit = async (e: Event) => {
		e.preventDefault();
		const form = e.currentTarget as HTMLFormElement;
		const isValid = form.checkValidity();
		if (!isValid) {
			return;
		}

		const content = html`
			<p>
				${this.localize.term("boo_confirmMessage")}
			</p>
			<ul>
				${this.types.filter(x => x.selected).map(x =>
					html`
						<li>${x.label}</li>
					`)}
			</ul>
			<p>
				<strong>${this.localize.term("boo_confirmWarning")}</strong>
			</p>
		`
		const modalContext = this.#modalManagerContext?.open(
			this, UMB_CONFIRM_MODAL,
			{
				data: {
					headline: `${this.localize.term("boo_confirmHeadline")}`,
					content: content,
					color: "danger"
				}
			}
		);

		modalContext
			?.onSubmit()
			.then(() => {
				this._confirmOrganise();
			})
			.catch(() => {
			});
	};

	_confirmOrganise = async () => {
		this.loading = true;
		const request = {
			contentTypes: this.contentTypes,
			mediaTypes: this.mediaTypes,
			memberTypes: this.memberTypes,
			dataTypes: this.dataTypes
		};

		const response = await this.#backofficeOrganiserContext?.organise(request);
		const data = response?.data;
		const success = !data?.error;
		const heading = success ? "Success" : "Error";
		const color = success ? "positive" : "danger";
		this.#notificationContext?.peek(color, {
			data: {
				message: data?.message ?? "",
				headline: heading
			}
		});

		this.types.forEach(x => x.selected = false);
		this.loading = false;
	};

	static styles = [
		css`
			.dashboard {
				padding: var(--uui-size-6);
			}

			.organise-type-container {
				display: flex;
				flex-direction: row;
				gap: var(--uui-size-3);
			}

			.organiser-type {
				margin-right: var(--uui-size-6);
			}

			.organiser-type uui-button {
				width: 100%;
			}
		`
	]

	private _getItems(value: OrganiseType) {
		let items = Array<OrganiseInfoModel>();
		switch (value) {
			case OrganiseType.ContentTypes:
				items = this.info?.contentTypes ?? [];
				break;
			case OrganiseType.MediaTypes:
				items = this.info?.mediaTypes ?? [];
				break;
			case OrganiseType.MemberTypes:
				items = this.info?.memberTypes ?? [];
				break;
			case OrganiseType.DataTypes:
				items = this.info?.dataTypes ?? [];
				break;
		}

		return items;
	}
}

declare global {
	interface HTMLElementTagNameMap {
		'backoffice-organiser': BackofficeOrganiser;
	}
}