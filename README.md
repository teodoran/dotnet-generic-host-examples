Sleep better with .NET Generic Host
===================================
_Are you tired of handling graceful shutdown manually by listening for key-events? Are you held awake at night by doubts of mishandled exceptions and restarts? Then this is the repository for you!_

Why should I care about Generic Host?
-------------------------------------
From time to time we write programs that acts as services, without being HTTP-APIs. Maybe we're reading messages from a queue, streaming events from a pipe or just simply keeping an open connection to something, either way we want a service that stays up when we want it, and shuts down cleanly.

It might be tempting to write such services as simple console applications, and just stick a `while(true)` or `Console.ReadLine()` in somewhere to keep it running. Sometimes this is sufficient, but is this always the best option?

.NET Core 2.1 introduced [.NET Generic Host](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host), and in this repository we'll have a look at some of the ways you can use Generic Host to create more robust services.

### Doesn't Kubernetes do all that work for me?
Kubernetes can do a lot of heavy lifting when it comes to [restarting failing programs](https://kubernetes.io/docs/tasks/configure-pod-container/configure-liveness-readiness-probes/) and keeping long [lived programs running](https://kubernetes.io/docs/concepts/workloads/controllers/replicaset/), but it still really want you to [handle termination of your program](https://kubernetes.io/docs/concepts/workloads/pods/pod/#termination-of-pods) in a clean and efficient manner.

### You won me over! How do I go about navigating this repository?
TODO: Get the content going


Closing notes
-------------
_"Generic Host will replace Web Host in a future release and act as the primary host API in both HTTP and non-HTTP scenarios."_

As stated in the [Generic Host documentation](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-2.2), Generic Host is the future of hosting in .NET Core. This means that we can expect that ASP.NET Core will switch over to using Generic Host some time in the future.
