// let data = 42;
// data = 'ali';

// let data;
// data = 'ali';

// let data;
// data = 'ali';
// data = 10;

// let data : any;
// data = 'ali';
// data = 10;

// let data : number;
// data = 'ali';
// data = 10;

let data: number | string;
data = 'ali';
data = 10;

interface ICar {
    color: string;
    model: string;
    topSpeed?: number;
}

const car1: ICar = {
    color: 'blue',
    model: 'bmw',
}
const car2: ICar = {
    color: 'red',
    model: 'mercedes',
    topSpeed: 100
}

const multiply = (x:any, y:any): number=> {
    return x * y;
};
