const routes = [
    {
        path: "/",
        component: Contest
    },
    {
        path: "ContestDetail/:id",
        name: "ContestDetail",
        component: ContestDetail,
        props: true
    }];