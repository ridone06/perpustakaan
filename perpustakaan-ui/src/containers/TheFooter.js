import React from 'react'
import { CFooter } from '@coreui/react'

const TheFooter = () => {
  return (
    <CFooter fixed={false}>
      <div className="mfs-auto">
        <span className="mr-1">Project author </span>
        <a href="#" rel="noopener noreferrer">Uti Ridwan Ali</a>
      </div>
    </CFooter>
  )
}

export default React.memo(TheFooter)
