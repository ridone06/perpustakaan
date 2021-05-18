import React from 'react'
import {
  CHeader,
  CHeaderNav,
  CHeaderNavItem,
  CSubheader,
  CBreadcrumbRouter,
  CLink
} from '@coreui/react'
import CIcon from '@coreui/icons-react'

// routes config
import routes from '../routes'

import {
  TheHeaderDropdown,
} from './index'

const TheHeader = () => {
  return (
    <CHeader withSubheader>

      <CHeaderNav className="d-md-down-none mr-auto">
        <CHeaderNavItem className="px-3" >
        </CHeaderNavItem>
        <CHeaderNavItem className="px-3">
        </CHeaderNavItem>
        <CHeaderNavItem className="px-3">
        </CHeaderNavItem>
      </CHeaderNav>

      <CHeaderNav className="px-3">
        <TheHeaderDropdown />
      </CHeaderNav>

      <CSubheader className="px-3 justify-content-between">
        <CBreadcrumbRouter
          className="border-0 c-subheader-nav m-0 px-0 px-md-3"
          routes={routes}
        />
      </CSubheader>
    </CHeader>
  )
}

export default TheHeader
