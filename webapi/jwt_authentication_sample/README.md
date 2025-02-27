# JWT Authentication on Fox Biz/Data Service Web API

이 예제는 JWT 인증을 사용하여 Fox Web Service Web API 를 구현하는 방법을 보여줍니다.

## Overview

JWT 토큰은 발행자가 대상(subject)에 대한 정보를 JSON 으로 기록하고 이 내용 변경되지 않았음을 확인하도록 디지털 서명을 포함합니다. JWT 에 대한 기본 개념은 다음 문서를 참고하십시요.

<https://jwt.io/introduction>

Fox Biz/Data Service Web API 는 인증 문자열(auth string)이라 부르는 암호화된 NeoDEEX 토큰을 사용한 기본적이고 간단한 인증 방식을 제공합니다. 하지만 이 방식은 표준화되지 않은 방법이며 폐쇠된 환경에서만 사용이 가능합니다.

반면 JWT 는 [RFC 7519](https://datatracker.ietf.org/doc/html/rfc7519)에 정의된 오픈 표준으로서 공개 환경에서 많이 사용되기 때문에 기업 내부 시스템에서도 JWT 기반 인증을 적용하는 경우가 많습니다.

이 예제는 JWT Bearer 인증을 적용하여 Fox Biz/Data Service Web API 를 구현하는 방법에 대해 설명합니다.

## Fox Authentication

Fox Biz/Data Service Web API 는 HTTP 헤더에 NeoDEEX 토큰이 존재하는지 여부에 따라 인증된 사용자의 서비스 호출 여부를 판단합니다.


---