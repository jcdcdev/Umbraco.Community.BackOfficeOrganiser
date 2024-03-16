export class Toast {
	message: string;
	headline: string;
	success: boolean;

	constructor(headline: string, message: string, success: boolean) {
		this.headline = headline;
		this.message = message;
		this.success = success;
	}
}