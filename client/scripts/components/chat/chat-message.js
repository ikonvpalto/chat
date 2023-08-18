import {Component} from "../component.js";

export default class ChatMessage extends Component {
    constructor() {
        super();
    }

    markup() {
        return `
            <p class="message-content"><slot name="message"></slot></p>
        `;
    }

    styles() {
        return `
            <style>
                :host {
                    display: block;
                    max-width: 20rem;
                    max-height: 20rem;
                    width: fit-content;
                    height: fit-content;

                    background: whitesmoke;
                    color: black;

                    padding: 1rem;
                    border-radius: 1rem;

                    box-sizing: border-box;
                }

                :host([data-type="received"]),
                :host,
                {
                    background: var(--color-message-received-bg);
                    color: var(--color-message-received);
                }

                :host([data-type="sent"]) {
                    background: var(--color-message-sent-bg);
                    color: var(--color-message-sent);
                    align-self: flex-end;
                }

                .message-content {
                    margin: 0;
                    overflow-y: scroll;
                    height: 100%;
                    word-wrap: break-word;
                }

                ::slotted(ul) {
                    margin: 0;
                }
            </style>
        `;
    }
}

customElements.define('chat-message', ChatMessage);
