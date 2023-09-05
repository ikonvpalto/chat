type Subscriber = (...args: any[]) => void;

export interface Subscription {
    unsubscribe(): void;
}

export class Observable {
    private _subscribers: Subscriber[] = [];

    addSubscriber(subscriber: Subscriber): Subscription {
        this._subscribers.push(subscriber);

        return {
            unsubscribe: () => {
                this._subscribers = this._subscribers.filter(o => o !== subscriber);
            }
        };
    }

    protected notifySubscribers(...args: any[]) {
        this._subscribers.forEach(subscriber => {
            subscriber(...args);
        });
    }
}
