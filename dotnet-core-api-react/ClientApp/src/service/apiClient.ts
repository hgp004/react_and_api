interface Event {
    message: string
}
let getClient: (baseUrl: string) => object

getClient = (url) => {
    return {};
}
class Handler {
    info: string = "init";
    onClickGood = (e: Event) => { this.info = e.message }
}
let handler1 = new Handler();
handler1.info = "123";
let handler2 = new Handler();
handler2.info = "234"
let a = {
    message: "aaa"
}
console.log("ff", handler1.onClickGood(a))
console.log("handler1.info", handler1.info, "handler2.info", handler2.info)


//类型兼容性
interface Named {
    name: string;
}

class Person {
    name: string = "";
}
class animal {
    type: string = "";
}
let p: Named;
// OK, because of structural typing
p = new Person();
//p = new animal();//error: 类型和名字都需要匹配
export default getClient