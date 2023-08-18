import {Component} from "../component.js";
import MessageStore from '../../stores/message-store.js';

export default class ChatBox extends Component {
    static get observedAttributes() {
        return ['messages'];
    }

    get messages() {
        if (!this._messages) {
            this._messages = MessageStore.data;
        }

        return this._messages;
    }

    set messages(value) {
        if (!Object.is(value, this._messages)) {
            this._messages = value;
            this.render();
        }
    }

    constructor() {
        super();
        MessageStore.addObserver(messages => this.messages = messages);
    }

    registerEvents() {
        this.shadowRoot
            .querySelector('message-box')
            .addEventListener('message-sent', this.onMessageSent.bind(this));
    }

    onMessageSent(event) {
        event.preventDefault();
        event.stopPropagation();

        MessageStore.addMessage({
            type: 'sent',
            message: event.detail.message,
        });
    }

    markup() {
        return `
            ${this.messages.map(this.messageMarkup).join('')}
            <message-box></message-box>
        `;
    }

    messageMarkup(message) {
        return `
            <chat-message data-type="${message.type}">
                <span slot="message">${message.message}</span>
            </chat-message>
        `;
    }

    styles() {
        return `
            <style>
                :host {
                    display: flex;
                    flex-direction: column;
                    gap: 1rem;
                    padding: 1rem;
                    height: 100%;

                    background: url("/assets/chat-bg.png");
                    background-repeat: repeat;
                    background-size: cover;
                }

                message-box {
                    margin-top: auto;
                }
            </style>
        `;
    }
}

customElements.define('chat-box', ChatBox);
