export default {
	boo: {
		selected: "Selected",
		disabled: "Disabled",
		numberOfOrganisers: (count: any) => {
			if ((parseInt(count) || 0) === 1) {
				return `${count} Organiser`;
			}
			return `${count} Organisers`;
		},
		organise: "Organise",
		contentTypes: "Content Types",
		mediaTypes: "Media Types",
		memberTypes: "Member Types",
		dataTypes: "Data Types",
		dashboardLabel: "Backoffice Organiser",
		title: "Welcome",
		confirmHeadline: "Organise Confirmation",
		confirmMessage: "Are you sure you want to organise the selected types?",
		confirmWarning: "This action cannot be undone.",
		selectTypes: "Select types to organise",
		selectTypesLabel: "Select types",
		requestFeature: "Request a Feature",
		reportBug: "Report a Bug",
		documentation: "Documentation",
		introduction: "This dashboard is designed to help you organise your Document Types, Media Types, Member Types and Data Types. To get started, select at least one type to organise and click the submit button.",
		organiseActionInfoIntroduction: "The following actions will be performed in order:",
	}
};
