export enum ActionType {
    Add = 50,
    Move = 30,
    Delete = 1
}
export namespace ActionType {

    export function values() {
        return Object.keys(ActionType).filter(
            (type) => isNaN(<any>type) && type !== 'values'
        );
    }
}