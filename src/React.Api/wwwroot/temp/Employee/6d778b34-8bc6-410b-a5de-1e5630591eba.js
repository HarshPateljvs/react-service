const arry = [5, 1, 3, 6, 10, 9];

// function double(x) {
//   return x * 2;
// }

// function binary(x) {
//   return x.toString(2);
// }

// const output = arry.map(binary);
// console.log(output);

// function isodd(x){
//   return x % 2;
// }
// const output = arry.filter((x) => x > 3);
// console.log(output);

//sum or max
function findsum(arry) {
  let sum = 0;
  for (let i = 0; i < arry.length; i++) {
    sum += arry[i];
  }
  return sum;
}


console.log(findsum(arry));

const output = arry.reduce(function (accumulator, currentValue) {
  return accumulator + currentValue;
},0);
console.log(output);

function findMax(arry) {
  let Max = 0;
  for (let i = 0; i < arry.length; i++) {
    if(arry[i] > Max){
      Max  = arry[i];
    }
  }
  return Max;
}
console.log("max",findMax(arry));

// const cart = ["apple", "banana", "orange"];

// createOrder(cart,function(orderid){
// processedToPaymnet(orderid);
// })

// const promise = createOrder(cart);

// const GITHUB_API = "https://api.github.com/users/HarshPateljvs";

// const user = fetch(GITHUB_API);
// console.log(user);

// user.then(function (response) {
//   console.log(response);
// });

// const myPromise = new Promise((resolve, reject) => {
//     setTimeout(() => {
//         resolve("Step 1 Result");
//     }, 1000);
// });

// myPromise
//     .then((step1Result) => {
//         console.log("First then:", step1Result); // Step 1 Result
//         return step1Result + " -> Step 2 Result"; // Pass this to next then
//     })
//     .then((step2Result) => {
//         console.log("Second then:", step2Result); // Step 1 Result -> Step 2 Result
//         return step2Result + " -> Step 3 Result"; // Pass to next then
//     })
//     .then((step3Result) => {
//         console.log("Third then:", step3Result); // Step 1 Result -> Step 2 Result -> Step 3 Result
//     });

// console.log("log 1");
// setTimeout(function () {
//   console.log("log 2");
// }, 4000);

// console.log("log 3");

// setTimeout(function () {
//   console.log("timer");
// }, 5000);

// function x(y, z) {
//   console.log("x");
//   y();
//   z();
// }
// x(
//   function y() {
//     console.log("y");
//   },
//   function z() {
//     console.log("z");
//   }
// );

// function eventhandler() {
//   let COunt = 0;
//   document.getElementById("event").addEventListener("click", function xyz() {
//     console.log("button clicked xyz", ++COunt);
//   });
// }
// eventhandler();

// var x = 1;
// a();
// b();
// console.log(x);
// function a() {
//   var x = 10;
//   if (true) {
//     var varra = 20;
//   }
//   varra = "varra chanfged from 20";
//   console.log(varra);
//   // console.log(x);
// }

// function b() {
//   var x = 100;
//   if (true) {
//     let letta = 20;
//     letta = "letta   chanfged from 20";
//     console.log(letta);

//     const consta = 30;
//     console.log(consta);
//   }
//   //console.log(x);
// }
