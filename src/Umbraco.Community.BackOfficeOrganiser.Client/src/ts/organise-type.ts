export class OrganiseType {
	value: number;
	label: string;
	selected: boolean;
	description: string;
	constructor(value: number, label: string, description :string, selected: boolean) {
		this.value = value;
		this.label = label;
		this.description = description;
		this.selected = selected;
	}
}