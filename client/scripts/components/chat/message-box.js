import {Component} from "../component.js";
import setupTextareaAutoResize from "../../utils/textarea-auto-resize.js";

export default class MessageBox extends Component {
    constructor() {
        super();
    }

    connectedCallback() {
        setupTextareaAutoResize(this.shadowRoot);
    }

    registerEvents() {
        this.shadowRoot
            .querySelector('a-button')
            .addEventListener('click', this.onMessageSent.bind(this));
    }

    onMessageSent(event) {
        event.preventDefault();
        event.stopPropagation();

        const tx = this.shadowRoot.querySelector('textarea');
        const message = tx.value;

        if (!message) {
            return;
        }

        this.fireEvent('message-sent', { message });
        tx.value = '';
    }

    markup() {
        return `
            <form class="message-box">
                <textarea class="message-box--input" placeholder="Type your message"></textarea>
                <a-button type="submit" data-type="round filled" onclick="${this.getAttribute('onsent')}">
                    <a-icon src="assets/chevron-right.svg" alt="Send message" slot="content" data-size="1.5"></a-icon>
                </a-button>
            </form>
        `;
    }

    styles() {
        return `
            <style>
                .message-box {
                    display: flex;
                    justify-content: space-between;
                    column-gap: 2rem;
                    align-items: end;
                }

                .message-box--input {
                    flex-grow: 1;
                    font-size: 1.125rem;
                    border-radius: 1rem;
                    padding: .5rem;
                    max-height: 9.25rem;
                }
            </style>
        `;
    }
}

customElements.define('message-box', MessageBox);
