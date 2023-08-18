export class Component extends HTMLElement {

    constructor(isOpened = true) {
        super();

        this.attachShadow({mode: isOpened ? 'open' : "closed"});
        this.render();
    }

    attributeChangedCallback() {
        this.render();
    }

    fireEvent(eventType, detail = null) {
        const event = detail
            ? new CustomEvent(eventType, {bubbles: true, composed: true, detail})
            : new Event(eventType, {bubbles: true, composed: true})
        this.shadowRoot.dispatchEvent(event);
    }

    render() {
        this.shadowRoot.innerHTML = `
            ${this.styles()}
            ${this.markup()}
        `;

        this.registerEvents();
    }

    markup() {
        return '';
    }

    styles() {
        return '';
    }

    registerEvents() {}

}
