import {LitElement, css, html, PropertyValues} from 'lit';
import {customElement} from 'lit/decorators.js';
import {setTextareaHeight, onTextareaValueChanged} from "../../utils/textarea-auto-resize";

@customElement('message-box')
export class MessageBox extends LitElement {

    protected firstUpdated(_changedProperties: PropertyValues) {
        super.firstUpdated(_changedProperties);

        setTextareaHeight((this.shadowRoot || this).querySelector('textarea'));
    }

    _onMessageSent(event: Event) {
        event.preventDefault();
        event.stopPropagation();

        const queryRoot = (this.shadowRoot || this);

        const message = queryRoot.querySelector('textarea')?.value;

        if (!message) {
            return;
        }

        this.dispatchEvent(new CustomEvent('message-sent', {detail: message}))

        queryRoot.querySelector('form')?.reset();
    }

    protected render() {
        return html`
            <form class="message-box">
                <textarea
                    class="message-box--input"
                    placeholder="Type your message"
                    @input=${onTextareaValueChanged}
                    @reset=${onTextareaValueChanged}
                ></textarea>
                <a-button data-modifiers="round filled" data-size="small" @click="${this._onMessageSent}">
                    <an-icon data-src="/images/chevron-right.svg" data-size="small" data-alt="Send message"></an-icon>
                </a-button>
            </form>
        `;
    }

    static styles = css`
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
    `;
}
